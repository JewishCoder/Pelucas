using DynamicData;
using DynamicData.Binding;
using Pelucas.Common.ApiProvider;
using Pelucas.Configuration;
using Pelucas.OmdbApi;
using Pelucas.UI.Models;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Pelucas.UI.ViewModels
{
	public class SearchViewModel : ReactiveObject
	{
		private string _searchQuery;

		private ApiProvider<OmdbApiParameters> OmdbProvider { get; }

		public string SearchQuery 
		{
			get => _searchQuery;
			set => this.RaiseAndSetIfChanged(ref _searchQuery, value);
		}

		public SourceList<MovieModel> Movies { get; }

		public IObservableCollection<MovieModel> Observable { get; private set; }

		public ReactiveCommand<Unit, Unit> SearchCommand { get; }

		public SearchViewModel()
		{
			var manager = new ConfigurationManager();
			OmdbProvider = ApiProviderFactory.Create<OmdbApiParameters>(manager.GetApiConfigPath());
			Movies = new SourceList<MovieModel>();
			Observable = new ObservableCollectionExtended<MovieModel>();
			SearchCommand = ReactiveCommand.CreateFromTask(SearchAsync, outputScheduler: RxApp.MainThreadScheduler);
			Movies.Connect()
				.ObserveOn(RxApp.MainThreadScheduler)
				.Bind(Observable)
				.DisposeMany()
				.Subscribe();
		}

		private async Task SearchAsync() 
		{
			Movies.Clear();
			if(SearchQuery == null) return;

			OmdbProvider.Parameters.SearchQuery = SearchQuery;
			var result = await OmdbProvider.ReceiveJsonDataAsync<OmbSearch>();
			try
			{
				if(result == null || result.Movies == null) return;
				foreach(var item in result.Movies)
				{
					var movie = new MovieModel
					{
						Title = item.Title,
						Year = item.Year,
					};

					if(item.Poster != null && !item.Poster.Equals("N/A"))
					{
						movie.Photo = new Uri(item.Poster);
					}
					Movies.Add(movie);
				}


			}
			catch(Exception exc) 
			{
				Movies.Add(new MovieModel { Title = exc.Message });
			}
		}


	}
}
