import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SearchComponent } from './features/search/search.component';
import { HistoryComponent } from './features/history/history.component';


const routes : Routes = [
  {
    path : '',
    redirectTo : '/search',
    pathMatch : 'full'
  },
  {
    path : 'search',
    component : SearchComponent
  },
  {
    path: 'history',
    component : HistoryComponent
  }
]



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [    //exporting the routes
    RouterModule
  ]
})
export class AppRoutingModule { }
