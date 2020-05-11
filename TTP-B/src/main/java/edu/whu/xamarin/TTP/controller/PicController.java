package edu.whu.xamarin.TTP.controller;

import edu.whu.xamarin.TTP.utils.StringUtils;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import java.io.File;
import java.io.IOException;

@Controller
@RequestMapping("/rest")
public class PicController {
    @CrossOrigin
    @PostMapping("api")
    public @ResponseBody
    String coversUpload(MultipartFile file) throws Exception {
        String folder = "C:/Users/79918/Desktop/work";
        File imageFolder = new File(folder);
        File f = new File(imageFolder, StringUtils.getRandomString(6) + file.getOriginalFilename()
                .substring(file.getOriginalFilename().length() - 4));
        if (!f.getParentFile().exists())
            f.getParentFile().mkdirs();
        try {
            file.transferTo(f);
            String imgURL = "http://192.168.1.6:8080/api/file/" + f.getName();
            return imgURL;
        } catch (IOException e) {
            e.printStackTrace();
            return "";
        }
    }
}
