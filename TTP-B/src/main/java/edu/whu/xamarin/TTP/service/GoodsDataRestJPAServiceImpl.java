package edu.whu.xamarin.TTP.service;


import edu.whu.xamarin.TTP.dao.GoodsDataRepository;
import edu.whu.xamarin.TTP.model.GoodsData;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;
import java.util.List;
import java.util.Optional;

@Slf4j
@Service
public class GoodsDataRestJPAServiceImpl implements GoodsDataRestService {

    @Resource
    private GoodsDataRepository goodsDataRepository;


    public GoodsData saveGoodsData(GoodsData goodsData) {

        goodsDataRepository.save(goodsData);

        return  goodsData;
    }

    @Override
    public void deleteGoodsData(Long id) {
        goodsDataRepository.deleteById(id);
    }

    @Override
    public void updateGoodsData(GoodsData goodsData) {
        goodsDataRepository.save(goodsData);
    }

    @Override
    public GoodsData getGoodsData(Long id) {
        Optional<GoodsData> goodsData = goodsDataRepository.findById(id);

        return goodsData.get();
    }

    @Override
    public List<GoodsData> getAll() {
        List<GoodsData> articleLis = goodsDataRepository.findAll();

        return articleLis;
    }
}
