package edu.whu.xamarin.TTP.service;

import edu.whu.xamarin.TTP.dao.TomatoTimeRepository;
import edu.whu.xamarin.TTP.model.TomatoTime;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;
import java.util.List;

@Service

public class TomatoTimeRestJPAServiceImpl implements TomatoTimeRestService {
    @Resource
    private TomatoTimeRepository tomatoTimeRepository;

    @Override
    public TomatoTime saveTomatoTime(TomatoTime tomatoTime) {
        tomatoTimeRepository.save(tomatoTime);
        return tomatoTime;
    }

    @Override
    public TomatoTime getTomatoTime(Long id) {
        return tomatoTimeRepository.findById(id).get();
    }

    @Override
    public List<TomatoTime> getTomatoTimeByUseId(Long id) {
        return tomatoTimeRepository.findByUserId(id);
    }
}
