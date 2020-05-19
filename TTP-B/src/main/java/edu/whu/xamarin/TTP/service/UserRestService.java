package edu.whu.xamarin.TTP.service;



import edu.whu.xamarin.TTP.model.User;

public interface UserRestService {
    User saveUser(User user);

    void deleteUser(Long id);

    void updateUser(User user);

    User getUser(String userName);

    User getUserById(Long id);
}
