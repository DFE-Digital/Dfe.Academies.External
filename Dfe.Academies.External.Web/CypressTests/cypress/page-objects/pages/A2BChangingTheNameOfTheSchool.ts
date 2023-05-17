import BasePage from "../BasePage"
export default class A2BChangingTheNameOfTheSchool extends BasePage {

    static changingTheNameOfTheSchoolElementsVisible()
    {
        cy.changingTheNameOfTheSchoolElementsVisible()
    }

    static submitChangingTheNameOfTheSchool()
    {
        cy.get('input[type="submit"]').click()
    }

}