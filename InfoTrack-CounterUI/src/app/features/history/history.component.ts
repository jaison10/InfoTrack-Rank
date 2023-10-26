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

  rankhistory : RankHistory [] = [];
  private searchRankSubscription ?: Subscription;

  ngOnDestroy(): void {
    this.searchRankSubscription?.unsubscribe();
  }
  ngOnInit(): void {
    this.searchRankSubscription = this.rankService.GetRankHistory().subscribe((rankResult)=>{
      this.rankhistory = rankResult;
    },(error)=>{
      console.log("Error occured while searching! ", error);
    });
  }

}
