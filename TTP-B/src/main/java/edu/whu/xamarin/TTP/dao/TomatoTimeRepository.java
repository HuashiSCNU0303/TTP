package edu.whu.xamarin.TTP.dao;

import edu.whu.xamarin.TTP.model.TomatoTime;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface TomatoTimeRepository extends JpaRepository<TomatoTime,Long> {

    List<TomatoTime> findByUserId(Long id);
}
