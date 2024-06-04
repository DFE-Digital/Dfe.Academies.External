import BasePage from ../basePage
export default class A2BHome extends BasePage {
/*  NOW CALLING THESE FROM NEW HEADER COMPONENT 
    static govUkHeaderVisible() {
        cy.get('.govuk-header__logotype').should('be.visible').contains('GOV.UK')
    }

    static applyToBecomeAnAcademyHeaderLinkVisible() {
        cy.get('.govuk-header__content').contains('Apply to become an Academy')
    }

*/

static homePageElementsVisible()
{
    cy.get('h1').contains('Apply to become an Academy')
    cy.get('p').eq(2).contains('This form is for local authority maintained schools that want to become academies.')
    cy.get('p').eq(3).contains('There is a separate process for:')
    cy.get('li').eq(0).contains('special schools')
    cy.get('li').eq(1).contains('pupil referral units (PRUs)')
    cy.get('p').eq(4).contains('Although you can invite other people to help with this application, only the chair of governors will be able to submit it.')
    cy.get('.govuk-heading-l').contains('Before you start')
    cy.get('p').eq(5).contains('You must:')
    cy.contains('carry out a consultation with your stakeholders')
    cy.contains('complete an Equality Impact Assessment (EIA)')
    cy.contains('provide bank details')
    cy.contains('You should also:')
    cy.contains('your regional director')
    cy.contains('all of the information and evidence you will need')
    cy.get('.govuk-grid-column-two-thirds > .govuk-button').should('be.visible')
    
}

    static clickStartNow() {
        cy.get('.govuk-grid-column-two-thirds > .govuk-button').click()
    }
}