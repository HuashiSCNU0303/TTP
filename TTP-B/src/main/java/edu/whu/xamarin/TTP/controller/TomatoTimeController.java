package edu.whu.xamarin.TTP.controller;

import edu.whu.xamarin.TTP.model.GoodsData;
import edu.whu.xamarin.TTP.model.TomatoTime;
import edu.whu.xamarin.TTP.model.User;
import edu.whu.xamarin.TTP.service.TomatoTimeRestService;
import edu.whu.xamarin.TTP.service.UserRestService;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import javax.annotation.Resource;
import java.util.List;

@Slf4j
@Controller
@RequestMapping("/rest")
public class TomatoTimeController {
    @Resource(name="tomatoTimeRestJPAServiceImpl")
    TomatoTimeRestService tomatoTimeRestService;

    @PostMapping("/tomatoTime")
    public @ResponseBody
    TomatoTime saveTomatoTime(@RequestBody TomatoTime tomatoTime) {
        tomatoTimeRestService.saveTomatoTime(tomatoTime);
        return  tomatoTime;
    }

    @GetMapping( "/tomatoTime/{id}")
    public @ResponseBody
    List<TomatoTime> getAllTomatoTimeByUserId(@PathVariable Long id) {
        return tomatoTimeRestService.getTomatoTimeByUseId(id);
    }
}
