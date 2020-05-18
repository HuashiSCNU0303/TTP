package edu.whu.xamarin.TTP.controller;

import edu.whu.xamarin.TTP.model.GoodsData;
import edu.whu.xamarin.TTP.model.User;
import edu.whu.xamarin.TTP.model.UserPackage;
import edu.whu.xamarin.TTP.service.UserPackageRestService;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import javax.annotation.Resource;

@Controller
@RequestMapping("/rest")
public class UserPackageController {
    @Resource(name="userPackageRestJPAServiceImpl")
    UserPackageRestService userPackageService;
    @PostMapping("/userPackage")
    public @ResponseBody
    UserPackage savePackage(@RequestBody UserPackage userPackage) {
        userPackageService.saveUserPackage(userPackage);
        return  userPackage;
    }

    @GetMapping("/userPackage/{userId}")
    public @ResponseBody
    UserPackage getPackage(@PathVariable Long userId) {
        UserPackage userPackage;
        try{
            userPackage= userPackageService.getUserPackageByUseId(userId);
        }catch (Exception e){
            userPackage=new UserPackage().builder().userId(userId).userPackage("").build();
            userPackageService.saveUserPackage(userPackage);
        }

        return userPackage ;
    }

    @PutMapping("/userPackage")
    public @ResponseBody
    UserPackage updateUserPackage(@RequestBody UserPackage userPackage) {
        long  userId=userPackage.getUserId();
        UserPackage userPackage2;
        try{
            userPackage2= userPackageService.getUserPackageByUseId(userId);
        }catch (Exception e){
            userPackage2=new UserPackage().builder().userId(userId).userPackage("").build();
            userPackageService.saveUserPackage(userPackage2);
            userPackage2= userPackageService.getUserPackageByUseId(userId);
        }
        userPackage.setId(userPackage2.getId());
        userPackageService.saveUserPackage(userPackage);
        return userPackage;
    }
}
