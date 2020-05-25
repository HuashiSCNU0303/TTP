package edu.whu.xamarin.TTP.controller;

import edu.whu.xamarin.TTP.model.AjaxResponse;
import edu.whu.xamarin.TTP.model.User;
import edu.whu.xamarin.TTP.model.UserPackage;
import edu.whu.xamarin.TTP.service.UserPackageRestService;
import edu.whu.xamarin.TTP.service.UserRestService;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import javax.annotation.Resource;
import java.util.List;

@Slf4j
@Controller
@RequestMapping("/rest")
public class UserController {
    @Resource(name="userPackageRestJPAServiceImpl")
    UserPackageRestService userPackageService;

    @Resource(name="userRestJPAServiceImpl")
    UserRestService userRestService;

    @PostMapping("/user")
    public @ResponseBody
    User saveUser(@RequestBody User user) {
        User user1= userRestService.saveUser(user);
        userPackageService.saveUserPackage(new UserPackage().builder().userId(user1.getUserId()).userPackage("com.companyname.ttp").build());
        return  user1;
    }

    @DeleteMapping("/user/{id}")
    public @ResponseBody
    AjaxResponse deleteUser(@PathVariable Long id) {

        userRestService.deleteUser(id);
        return AjaxResponse.success(id);
    }

    @PutMapping("/user")
    public @ResponseBody User updateUser(@RequestBody User user) {

        userRestService.updateUser(user);
        return user;
    }

    @GetMapping( "/user/api/{userName}")
    public @ResponseBody User getUserById(@PathVariable String userName) {
        return userRestService.getUser(userName);
    }

    @GetMapping( "/user/{id}")
    public @ResponseBody User getUser(@PathVariable Long id) {
        return userRestService.getUserById(id);
    }
}
