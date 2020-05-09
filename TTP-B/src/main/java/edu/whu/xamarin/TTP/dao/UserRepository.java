package edu.whu.xamarin.TTP.dao;

import edu.whu.xamarin.TTP.model.User;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.Optional;


public interface UserRepository extends JpaRepository<User,Long> {


}