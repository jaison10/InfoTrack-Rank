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
    host:'',
    url : '',
    positions : '',
    searchString : '',
    searchEngineId : '',
    storedInDB : false
  };

  urlInitial : string = 'www.';
  engines : Engine[] = [];
  hideResultSec : boolean = true;
  rankSec : boolean = true;
  disableSearchButton : boolean = false;
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
      // assigning the default Search Engine
      if(this.engines.length > 0){
        this.rankSearch.searchEngineId = this.engines[0].id;
      }
    }, (error)=>{
      console.log("Error while fetching search engines! ", error);
    })
  }

  searchForRank(){
    this.HideTheResult();

    console.log("Value going to update : ", this.rankSearch);
    // disabling the search button
    this.disableSearchButton = true;
    // combining to prepare the URL.
    this.rankSearch.url = this.urlInitial + this.rankSearch.host;
    this.searchRankSubscription = this.rankService.SearchForRank(this.rankSearch).subscribe((rankResult)=>{
      this.rankSearch = rankResult;
      this.hideResultSec = false;
      // customizing the result to be displayed to be more user friendly.
      if(rankResult.positions != null && rankResult.positions.length > 0){
        this.rankSec = false;
        this.RankResult = 'Search Was Successful!';
        if(rankResult.storedInDB == true) this.RankResult += ' The History Has Been Recorded!'
      }else{
        this.RankResult = 'Search Was Successful With No Ranks!';
      }
      // enabling the search button
      this.disableSearchButton = false;
    },(error)=>{
      console.log("Error occured while searching! ", error);
      if(error.status ==  400){
        this.RankResult = 'Invalid Input!';
      }else{
        this.RankResult = 'Search Was Unsuccessful!';
      }
      this.hideResultSec = false;
      this.rankSec = true;
      // enabling the search button
      this.disableSearchButton = false;
    });
  }

  onInputChange(){
    this.HideTheResult();
  }
  HideTheResult(){
    // hiding the result section
    if(this.hideResultSec == false){
      this.hideResultSec = true;
      this.rankSec = true;
      this.rankSearch.positions = '';
    }
  }
}
