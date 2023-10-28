import { Component, OnDestroy, OnInit } from '@angular/core';
import { RankHistory } from '../models/rank-history-model';
import { RankService } from '../rank.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit, OnDestroy {

  constructor(private rankService : RankService){}

  filterString : string = '';
  rankhistory : RankHistory [] = [];
  filteredRanks = [...this.rankhistory]; // Create a copy for filtering
  private searchRankSubscription ?: Subscription;

  ngOnDestroy(): void {
    this.searchRankSubscription?.unsubscribe();
  }
  ngOnInit(): void {
    this.searchRankSubscription = this.rankService.GetRankHistory().subscribe((rankResult)=>{
      this.rankhistory = rankResult;
      this.filteredRanks = rankResult;
    },(error)=>{
      console.log("Error occured while searching! ", error);
    });
  }
  filterRanks(){
    if(this.filterString.length > 0){
      this.filteredRanks = this.rankhistory.filter(item =>
        item.url.toLowerCase().includes(this.filterString.toLowerCase()) ||
        item.searchString.toLowerCase().includes(this.filterString.toLowerCase())
      );
    }else{
      this.filteredRanks = this.rankhistory;
    }  
  }
}
