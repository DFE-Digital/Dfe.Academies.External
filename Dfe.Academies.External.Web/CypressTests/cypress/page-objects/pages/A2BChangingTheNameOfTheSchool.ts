import BasePage from ../basePage
export default class A2BChangingTheNameOfTheSchool extends BasePage {

    static changingTheNameOfTheSchoolElementsVisible()
    {
        cy.changingTheNameOfTheSchoolElementsVisible()
    }

    static changingTheNameOfTheSchoolSelectOptionNo()
    {
    cy.get('#selectoptionNo').click()
    cy.get('#selectoptionNo').should('be.checked')
    }

    static submitChangingTheNameOfTheSchool()
    {
        cy.get('input[type=submit]').click()
    }

}