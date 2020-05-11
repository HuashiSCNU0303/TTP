package edu.whu.xamarin.TTP.model;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.Table;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
@Entity
@Table(name="userTable")
public class User {
    @Id
    @GeneratedValue
    private Long userId;
    private String userName;
    private int tomatoPoints;
    private String imgurl;
    private String passWord;
}
