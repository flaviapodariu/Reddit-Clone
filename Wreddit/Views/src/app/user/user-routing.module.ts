import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
const routes: Routes = [
  {
    path: "user",
    loadChildren: () => import('./user.module').then(m => m.UserModule)
  },
  { path: 'user-dashboard', component: UserDashboardComponent}
];


@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes),
            CommonModule],
  exports: [RouterModule]
})
export class UserRoutingModule { }
