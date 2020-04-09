package edu.whu.xamarin.TTP.dao;

import edu.whu.xamarin.TTP.model.GoodsData;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface GoodsDataRepository extends JpaRepository<GoodsData,Long> {

    Optional<GoodsData> findById(Long id);

}