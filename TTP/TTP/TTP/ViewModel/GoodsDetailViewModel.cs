using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TTP.Model;

namespace TTP.ViewModel
{
    class GoodsDetailViewModel : ViewModelBase
    {
        private GoodsModel goodsModel;
        public GoodsDetailViewModel()
        {
            goodsModel = new GoodsModel();
        }

        public GoodsModel GoodsModel
        {
            get { return goodsModel; }
            set { goodsModel = value; RaisePropertyChanged(); }
        }
    }
}