package edu.whu.xamarin.TTP.service;

import edu.whu.xamarin.TTP.dao.GoodsDataRepository;
import edu.whu.xamarin.TTP.dao.UserRepository;
import edu.whu.xamarin.TTP.model.GoodsData;
import edu.whu.xamarin.TTP.model.User;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;
import java.util.List;
import java.util.Optional;

@Service
public class UserRestJPAServiceImpl implements UserRestService {
    @Resource
    private UserRepository userRepository;


    @Override
    public User saveUser(User user) {
        userRepository.save(user);
        return user;
    }

    @Override
    public void deleteUser(Long id) {
        userRepository.deleteById(id);
    }

    @Override
    public void updateUser(User user) {
        userRepository.save(user);
    }

    @Override
    public User getUser(String userName) {
        return userRepository.getByUserName(userName);
    }

    @Override
    public User getUserById(Long id) {
        return userRepository.findById(id).get();
    }
}
