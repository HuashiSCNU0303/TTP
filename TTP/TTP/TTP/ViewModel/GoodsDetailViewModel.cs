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
            AddCommand = new RelayCommand<GoodsModel>(g=>AddGoods(g));
            DeleteCommand = new RelayCommand<GoodsModel>(d => DeleteGoods(d));
        }

        public GoodsModel GoodsModel
        {
            get { return goodsModel; }
            set { goodsModel = value; RaisePropertyChanged(); }
        }

        public RelayCommand<GoodsModel> AddCommand { get; private set; }
        public RelayCommand<GoodsModel> DeleteCommand { get; private set; }

        public void AddGoods(GoodsModel addGoodsModel) {
            GoodsViewModel.add(addGoodsModel);
        }

        public void DeleteGoods(GoodsModel deleteGoodsModel)
        {
            GoodsViewModel.delete(deleteGoodsModel);
        }
    }
}