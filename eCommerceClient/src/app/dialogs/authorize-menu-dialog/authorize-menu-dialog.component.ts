import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { BaseDialog } from '../base/base-dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { RoleService } from '../../services/common/models/role.service';
import { List_Role } from '../../contracts/role/List_Role';
import { MatSelectionList } from '@angular/material/list';

@Component({
  selector: 'app-authorize-menu-dialog',
  templateUrl: './authorize-menu-dialog.component.html',
  styleUrl: './authorize-menu-dialog.component.scss'
})
export class AuthorizeMenuDialogComponent extends BaseDialog<AuthorizeMenuDialogComponent> implements OnInit {
  constructor(dialogRef: MatDialogRef<AuthorizeMenuDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AuthorizeMenuState | any,
    private roleService: RoleService
  ) {
    super(dialogRef);
  }
  roles: { datas: List_Role[], totalCount: number };

  async ngOnInit() {
    this.roles = await this.roleService.getRoles(-1, -1);
  }

  assignRoles(rolesComponent: MatSelectionList) {
    const roles: string[] = rolesComponent.selectedOptions.selected.map(o => o._elementRef.nativeElement.innerText)
  }

}



export enum AuthorizeMenuState {
  Yes,
  No,
}
