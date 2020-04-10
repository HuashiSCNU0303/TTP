using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using TTP.Model;

namespace TTP.ViewModel
{
    class AddGoodsViewModel : ViewModelBase
    {
        private GoodsModel goodsModel;
        public AddGoodsViewModel()
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
