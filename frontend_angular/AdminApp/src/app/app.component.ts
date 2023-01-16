import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'AdminApp';
  options = this._formBuilder.group({
    fixed: true,
  });
  constructor(private _formBuilder: FormBuilder) {}
}
