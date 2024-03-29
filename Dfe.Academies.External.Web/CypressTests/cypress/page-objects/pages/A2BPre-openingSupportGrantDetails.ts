import BasePage from "../BasePage"
export default class A2BPreopeningSupportGrantDetails extends BasePage {

static preopeningSupportGrantDetailsElementsVisible()
{
    cy.preopeningSupportGrantDetailsElementsVisible()
}

static selectToTheSchoolVerifyAndSubmitPreopeningSupportGrantDetails()
{
    cy.get('#pay-toSchool').click()
    cy.verifyToTheSchoolPreopeningSupportGrantDetailsSectionDisplays()
    cy.get('input[type="submit"]').click()
}

static FAMSelectToTheSchoolVerifyAndSubmitPreopeningSupportGrantDetails()
{
    cy.get('#ConfirmSchoolPay').click()
    cy.get('#ConfirmSchoolPay').should('be.checked')

    cy.get('input[type="submit"]').click()
}

}