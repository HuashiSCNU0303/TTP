package edu.whu.xamarin.TTP.service;


import edu.whu.xamarin.TTP.model.GoodsData;

import java.util.List;

public interface GoodsDataRestService {

     GoodsData saveGoodsData(GoodsData goodsData);

     void deleteGoodsData(Long id);

     void updateGoodsData(GoodsData goodsData);

     GoodsData getGoodsData(Long id);

     List<GoodsData> getAll();
}