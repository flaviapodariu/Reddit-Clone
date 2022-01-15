import { Component, OnInit } from '@angular/core';
import { AdminService, allUsers } from 'src/app/services/admin.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss']
})
export class AdminDashboardComponent implements OnInit {

  constructor(private adminService: AdminService) { }
   users: allUsers[] = [];
  ngOnInit(): void {
   this.adminService.getAllUsers().subscribe((res: any)=>{
     console.log(res);
     if(res)
      this.users = res;
    else alert("Could not get users!");
   });
  }
  getAll()
  {
    return this.adminService.getAllUsers();
  }
 
}
