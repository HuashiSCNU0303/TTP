package edu.whu.xamarin.TTP.service;

import edu.whu.xamarin.TTP.dao.TomatoTimeRepository;
import edu.whu.xamarin.TTP.dao.UserPackageRepository;
import edu.whu.xamarin.TTP.model.UserPackage;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;

@Service
public class UserPackageRestJPAServiceImpl implements UserPackageRestService {
    @Resource
    private UserPackageRepository userPackageRepository;
    @Override
    public UserPackage saveUserPackage(UserPackage userPackage) {
        userPackageRepository.save(userPackage);
        return userPackage;
    }

    @Override
    public UserPackage getUserPackageByUseId(Long userId) {

        return userPackageRepository.getUserPackageByUserId(userId);
    }
}
