import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-app-button-primary',
  template: `
    <button
      class="ttnc-ButtonPrimary disabled:bg-opacity-90 bg-slate-900 dark:bg-slate-100 hover:bg-slate-800 text-slate-50 dark:text-slate-800 shadow-xl"
      [ngClass]="className"
    >
      <ng-content></ng-content>
    </button>
  `,
  standalone: true,
  imports: [CommonModule],
})
export class AppButtonPrimaryComponent {
  @Input() className = '';
}
