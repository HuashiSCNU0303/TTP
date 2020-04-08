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
            AddCommand = new RelayCommand<GoodsModel>(g => AddGoods(g));
        }

        public GoodsModel GoodsModel
        {
            get { return goodsModel; }
            set { goodsModel = value; RaisePropertyChanged(); }
        }

        public RelayCommand<GoodsModel> AddCommand { get; private set; }

        public void AddGoods(GoodsModel addGoodsModel)
        {
            GoodsViewModel.add(addGoodsModel);
        }
    }
}
