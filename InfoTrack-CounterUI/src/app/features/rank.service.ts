import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Rank } from './models/rank-model';
import { Observable } from 'rxjs';
import { SearchRankRequest } from './models/search-request-model';
import { RankHistory } from './models/rank-history-model';
import { Engine } from './models/engine-model';

@Injectable({
  providedIn: 'root'
})
export class RankService {

  private URL = 'https://localhost:7056';

  constructor(private httpClient : HttpClient) { }

  GetSearchEngines() : Observable<Engine[]>{
    return this.httpClient.get<Engine[]>(this.URL + "/SearchEngine");
  }

  SearchForRank(rankSearch : Rank):Observable<Rank>{
    var rankRequest : SearchRankRequest = {
      url : rankSearch.url,
      searchString : rankSearch.searchString,
      searchEngineId : rankSearch.searchEngineId
    };
    return this.httpClient.post<Rank>(this.URL + "/Search", rankRequest);
  }

  GetRankHistory() : Observable<RankHistory[]>{
    return this.httpClient.get<RankHistory[]>(this.URL + "/History");
  }
}
