package edu.whu.xamarin.TTP.controller;


import edu.whu.xamarin.TTP.model.AjaxResponse;
import edu.whu.xamarin.TTP.model.GoodsData;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;


@Slf4j
@Controller
@RequestMapping("/rest")
public class GoodsDataRestController {


    @PostMapping("/goodsData")
    public @ResponseBody
    AjaxResponse saveGoodsData(@RequestBody GoodsData goodsData) {

        log.info("saveGoodsData：{}",goodsData);

        return  AjaxResponse.success(goodsData);
    }

    @DeleteMapping("/goodsData/{id}")
    public @ResponseBody AjaxResponse deleteGoodsData(@PathVariable Long id) {

        log.info("deleteArticle：{}",id);

        return AjaxResponse.success(id);
    }

    @PutMapping("/goodsData/{id}")
    public @ResponseBody AjaxResponse updateGoodsData(@PathVariable Long id, @RequestBody GoodsData goodsData) {
        goodsData.setId(id);

        log.info("updateGoodsData：{}",goodsData);

        return AjaxResponse.success(goodsData);
    }

    @GetMapping( "/goods-data/{id}")
    public @ResponseBody
    List<GoodsData> getGoodsData(@PathVariable Long id) {

        DateFormat format = new SimpleDateFormat("yyyy-MM-dd HH:mm");

        try{
            GoodsData goodsData1 = GoodsData.builder()
                    .id(id)
                    .goodsId(2L)
                    .name("测试商品")
                    .type(GoodsData.TYPE.AAA)
                    .price(12)
                    .owner("我自己")
                    .date(format.parse("2019-10-20 12:12"))
                    .description("一个用来测试的商品")
                    .build();
//            return AjaxResponse.success(goodsData1);
            List<GoodsData> list=new ArrayList<GoodsData>();
            list.add(goodsData1);
            return list;
        }catch (ParseException e){
            return null;
        }


    }
}