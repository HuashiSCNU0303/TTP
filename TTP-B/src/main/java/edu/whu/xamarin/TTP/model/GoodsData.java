package edu.whu.xamarin.TTP.model;

import com.fasterxml.jackson.annotation.JsonFormat;
import com.fasterxml.jackson.annotation.JsonIgnore;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.Date;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder

public class GoodsData {
    public enum TYPE { AAA, BBB, CCC };
    @JsonIgnore
    private Long goodsId;
    private Long id;
    private String name;
    private int price;
    private TYPE type;
    public String owner;
    @JsonFormat(pattern = "yyyy-MM-dd HH:mm", timezone = "GMT+8")
    private Date date;
    private String description;
}
