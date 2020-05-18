package edu.whu.xamarin.TTP.service;


import edu.whu.xamarin.TTP.model.UserPackage;

import java.util.List;

public interface UserPackageRestService {
    UserPackage saveUserPackage(UserPackage userPackage);
    UserPackage getUserPackageByUseId(Long UserId);
}
