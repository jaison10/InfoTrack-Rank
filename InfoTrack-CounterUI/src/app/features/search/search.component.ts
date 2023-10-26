import { Component, OnDestroy, OnInit } from '@angular/core';
import { Rank } from '../models/rank-model';
import { Subscription } from 'rxjs';
import { RankService } from '../rank.service';

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
    positions : '1, 2, 3, 15, 35',
    searchString : ''
  };
  hideResultSec : boolean = true;
  rankSec : boolean = true;
  RankResult : string = '';

  private searchRankSubscription ?: Subscription;

  ngOnDestroy(): void {
    this.searchRankSubscription?.unsubscribe();
  }
  ngOnInit(): void {
  }

  searchForRank(){
    this.HideTheResult();

    this.searchRankSubscription = this.rankService.SearchForRank(this.rankSearch).subscribe((rankResult)=>{
      console.log("RETURN VALUE : ", rankResult);
      
      this.rankSearch = rankResult;
      this.hideResultSec = false;
      if(rankResult.positions != null && rankResult.positions.length > 0){
        console.log("RANK EXISTS ");
        
        this.rankSec = false;
        this.RankResult = 'Search Was Successful!';
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
    console.log("TYPED!");
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
