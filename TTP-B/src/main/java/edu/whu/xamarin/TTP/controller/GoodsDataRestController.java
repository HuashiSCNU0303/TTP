package edu.whu.xamarin.TTP.controller;


import edu.whu.xamarin.TTP.model.AjaxResponse;
import edu.whu.xamarin.TTP.model.GoodsData;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;


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

    @GetMapping( "/goodsData/{id}")
    public @ResponseBody  AjaxResponse getGoodsData(@PathVariable Long id) {

        GoodsData goodsData1 = GoodsData.builder()
                .id(1L)
                .goodsId(2L)
                .goodsPrice(2)
                .carrierId(3L)
                .carrierName("张俊杰")
                .date("4")
                .category("5")
                .goodsDescription("一个用来测试的商品")
                .build();
        return AjaxResponse.success(goodsData1);
    }
}