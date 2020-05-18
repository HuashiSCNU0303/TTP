package edu.whu.xamarin.TTP.dao;

import edu.whu.xamarin.TTP.model.UserPackage;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface UserPackageRepository extends JpaRepository<UserPackage,Long> {
    UserPackage getUserPackageByUserId(long userId);
}
