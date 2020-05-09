package edu.whu.xamarin.TTP.service;

import edu.whu.xamarin.TTP.model.TomatoTime;

import java.util.List;

public interface TomatoTimeRestService {
    TomatoTime saveTomatoTime(TomatoTime tomatoTime);
    TomatoTime getTomatoTime(Long id);
    List<TomatoTime> getTomatoTimeByUseId(Long id);
}
