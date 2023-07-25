import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{

  banner: Banner[] = [];
 
  
  constructor(private primengConfig: PrimeNGConfig){
  }
    
  ngOnInit() {
    this.banner = [
        {
           
            image:
                'assets/mission.PNG',
                  },
                  {
                      
             image:
                'assets/vission.PNG',
                },
    ];
}

}

export interface Banner {
  image?: String;
}
