package com.example.demo;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.ArrayList;


import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;




@RestController
public class PropertyController {
   private final ApplicationProperty applicationProperty;
    private final DeveloperProperty developerProperty;

    @Autowired
    public PropertyController(
           ApplicationProperty applicationProperty,
            DeveloperProperty developerProperty) {
       this.applicationProperty = applicationProperty;
        this.developerProperty = developerProperty;
    }

    @GetMapping("/property")
    public String index() {

     //   ArrayList<Object> array = new ArrayList<Object>(2);



    //   array.set(0, applicationProperty);
      //  array.set(1, developerProperty);
        return applicationProperty.getName()+"      "+developerProperty.getPhoneNumber();
    }
}
