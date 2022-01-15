import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';

const routes: Routes = [
  {
    path: "restricted",
    loadChildren: () => import('./admin.module').then(m => m.AdminModule)
  },
  { path: 'admin-dashboard', component: AdminDashboardComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes),
            CommonModule],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
