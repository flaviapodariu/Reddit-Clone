import { Component, OnInit } from '@angular/core';
import { AdminService, allUsers } from 'src/app/services/admin.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.scss']
})
export class AdminDashboardComponent implements OnInit {

  constructor(private adminService: AdminService,
              public authService: AuthenticationService,
              private userService: UserService) { }

  users: allUsers[] = [];
  deletedUser: string = "";

  ngOnInit(): void {
   this.adminService.getAllUsers().subscribe((res: any)=>{
     console.log(res);
     if(res)
      this.users = res;
    else alert("Could not get users!");
   });
  }
 
  deleteUser(userToDelete: number)
  {
     this.userService.deleteUser(userToDelete).subscribe((res:any)=>{
        if(res)
          this.deletedUser = res; 
     }
     );

  }
}
