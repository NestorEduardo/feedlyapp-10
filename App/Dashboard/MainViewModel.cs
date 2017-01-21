﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using App.Services.OAuth;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace App.Dashboard
{
    public class MainViewModel : ViewModelBase
    {
        private StreamDto _stream;
        public StreamDto Stream { get { return _stream;  } set { Set(ref _stream, value); } }

        private List<Subscrition> _subscription;
        private Subscrition _selected;
        private FeedlyApi _feedlyApi;

        public List<Subscrition> Subscritions {
            get { return _subscription; }
            set
            {
                Set(ref _subscription, value);
            }
        }

        public Subscrition SelectedSubscrition
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ICommand FetchFeedCommand { get; set; }

        public MainViewModel()
        {
            FetchFeedCommand = new RelayCommand(async () => await FetchFeed());
        }

        private async Task FetchFeed()
        {
            Stream = await _feedlyApi.GetContent(SelectedSubscrition.Id);
        }

        public async Task OnStart(OAuthToken oAuthToken)
        {
            _feedlyApi = new FeedlyApi(oAuthToken);
            var subscription = await _feedlyApi.GetSubscrition();
            Subscritions = subscription;
        }
    }
}
