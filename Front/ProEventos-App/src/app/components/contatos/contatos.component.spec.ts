import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { Component, Input } from '@angular/core';
import { By } from '@angular/platform-browser';
import { ContatosComponent } from './contatos.component';

@Component({
  selector: 'app-titulo',
  template: ''
})
class AppTituloStubComponent {
  @Input() titulo!: string;
  @Input() iconClass!: string;
}

describe('ContatosComponent', () => {
  let component: ContatosComponent;
  let fixture: ComponentFixture<ContatosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContatosComponent, AppTituloStubComponent ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContatosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('deve passar os inputs corretos para app-titulo', () => {
    const tituloDebugElement = fixture.debugElement.query(By.css('app-titulo'));
    const tituloComponentInstance = tituloDebugElement.componentInstance as AppTituloStubComponent;

    expect(tituloComponentInstance.titulo).toBe('Contatos');
    expect(tituloComponentInstance.iconClass).toBe('fas fa-envelope-open-text');
  });
});
