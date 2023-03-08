import BasePage from "../BasePage"
export default class A2BWhatIsTheNameOfTheSchool extends BasePage {

static whatIsTheNameOfTheSchoolElementsVisible() {
    cy.whatIsTheNameOfTheSchoolElementsVisible()
}

static enterSchoolNameSelectAndSubmit() {
cy.enterSchoolNameSelectAndSubmit()
}

static changeSchoolName() {
    cy.changeSchoolName()
}

}