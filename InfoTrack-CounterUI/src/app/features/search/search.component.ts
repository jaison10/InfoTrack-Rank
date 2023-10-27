import { Component, OnDestroy, OnInit } from '@angular/core';
import { Rank } from '../models/rank-model';
import { Subscription } from 'rxjs';
import { RankService } from '../rank.service';
import { Engine } from '../models/engine-model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit, OnDestroy {
  
  constructor(private rankService : RankService){}

  rankSearch : Rank = {
    id : '',
    url : '',
    positions : '',
    searchString : '',
    searchEngineId : '',
    storedInDB : false
  };

  engines : Engine[] = [];
  hideResultSec : boolean = true;
  rankSec : boolean = true;
  RankResult : string = '';

  private searchRankSubscription ?: Subscription;
  private engineSubscription ?: Subscription;

  ngOnDestroy(): void {
    this.searchRankSubscription?.unsubscribe();
    this.engineSubscription?.unsubscribe();
  }
  ngOnInit(): void {
    this.engineSubscription = this.rankService.GetSearchEngines().subscribe((engines)=>{
      this.engines = engines
      console.log("Obtained Engines : ", this.engines);
      
    }, (error)=>{
      console.log("Error while fetching search engines! ", error);
    })
  }

  searchForRank(){
    this.HideTheResult();

    console.log("Value going to update : ", this.rankSearch);
    
    this.searchRankSubscription = this.rankService.SearchForRank(this.rankSearch).subscribe((rankResult)=>{
      console.log("RETURN VALUE : ", rankResult);
      
      this.rankSearch = rankResult;
      this.hideResultSec = false;
      if(rankResult.positions != null && rankResult.positions.length > 0){
        this.rankSec = false;
        this.RankResult = 'Search Was Successful!';
        if(rankResult.storedInDB == true) this.RankResult += ' The History Has Been Recorded!'
      }else{
        this.RankResult = 'Search Was Successful With No Ranks!';
      }
      
    },(error)=>{
      console.log("Error occured while searching! ", error);
      if(error.status ==  400){
        this.RankResult = 'Invalid Input!';
      }else{
        this.RankResult = 'Search Was Unsuccessful!';
      }
      this.hideResultSec = false;
      this.rankSec = true;
    });
  }

  onInputChange(){
    this.HideTheResult();
  }
  HideTheResult(){
    if(this.hideResultSec == false){
      this.hideResultSec = true;
      this.rankSec = true;
      this.rankSearch.positions = '';
    }
  }
}
