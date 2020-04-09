package edu.whu.xamarin.TTP.controller;


import edu.whu.xamarin.TTP.model.AjaxResponse;
import edu.whu.xamarin.TTP.model.GoodsData;
import edu.whu.xamarin.TTP.service.GoodsDataRestService;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import javax.annotation.Resource;
import java.util.List;


@Slf4j
@Controller
@RequestMapping("/rest")
public class GoodsDataRestController {

    @Resource(name="goodsDataRestJPAServiceImpl")
    GoodsDataRestService goodsDataRestService;

    @PostMapping("/goods-data")
    public @ResponseBody GoodsData saveGoodsData(@RequestBody GoodsData goodsData) {

        log.info("saveGoodsData：{}",goodsData);
        log.info("goodsDataRestService return :" + goodsDataRestService.saveGoodsData(goodsData));

        return  goodsData;
    }

    @DeleteMapping("/goods-data/{id}")
    public @ResponseBody AjaxResponse deleteGoodsData(@PathVariable Long id) {

        log.info("deleteArticle：{}",id);
        goodsDataRestService.deleteGoodsData(id);
        return AjaxResponse.success(id);
    }

    @PutMapping("/goods-data")
    public @ResponseBody GoodsData updateGoodsData(@RequestBody GoodsData goodsData) {

        log.info("updateGoodsData：{}",goodsData);
        goodsDataRestService.updateGoodsData(goodsData);
        return goodsData;
    }

    @GetMapping( "/goods-data")
    public @ResponseBody List<GoodsData> getAllGoodsData() {
        return goodsDataRestService.getAll();
    }

    @GetMapping( "/goods-data/{id}")
    public @ResponseBody GoodsData getGoodsDataById(@PathVariable Long id) {
        return goodsDataRestService.getGoodsData(id);
    }
}