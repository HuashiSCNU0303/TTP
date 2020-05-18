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
@Table(name="userPackage")
public class UserPackage {
    @Id
    @GeneratedValue
    private Long id;

    private Long userId;

    private String userPackage;
}
