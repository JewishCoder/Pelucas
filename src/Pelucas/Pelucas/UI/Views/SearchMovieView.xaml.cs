using Pelucas.UI.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Pelucas.UI.Views
{
	/// <summary>
	/// Interaction logic for SearchMovieView.xaml
	/// </summary>
	public partial class SearchMovieView
	{
		public SearchMovieView()
		{
			InitializeComponent();
			ViewModel = new SearchViewModel();
			this.WhenActivated(disposable =>
			{
				this.Bind(ViewModel, x => x.SearchQuery, x => x.SearchQuery.Text)
					.DisposeWith(disposable);
				this.BindCommand(ViewModel, x => x.SearchCommand, x => x.Search)
					.DisposeWith(disposable);
				this.OneWayBind(ViewModel, x => x.Observable, x => x.Movies.ItemsSource);
			});
		}
	}
}
