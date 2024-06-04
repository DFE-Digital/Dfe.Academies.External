import BasePage from ../basePage
export default class A2BConsultationDetails extends BasePage {

static consultationDetailsElementsVisible()
{
    cy.consultationDetailsElementsVisible()
}

static selectHasGovBodyConsultedStakeholdersOptionNo()
{
    cy.get('#consultationStakeholdersOptionNo').click()
    cy.get('#consultationStakeholdersOptionNo').should('be.checked')
}

static fillConsultationDetails()
{
    cy.fillConsultationDetails()
}

static submitConsultationDetails()
{
    cy.submitConsultationDetails()
}

}