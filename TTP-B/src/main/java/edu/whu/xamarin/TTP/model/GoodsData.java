package edu.whu.xamarin.TTP.model;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class GoodsData {
    private Long id;
    private Long goodsId;
    private int goodsPrice;
    private Long carrierId;
    private String carrierName;
    private String date;
    private String category;
    private String goodsDescription;
}
