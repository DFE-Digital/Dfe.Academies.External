import BasePage from "../BasePage"
export default class A2BHome extends BasePage {

    static govUkHeaderVisible() {
        cy.get('.govuk-header__logotype').should('be.visible').contains('GOV.UK')
    }

    static applyToBecomeAnAcademyHeaderLinkVisible() {
        cy.get('.govuk-header__content').contains('Apply to become an Academy')
    }

    static h1ApplyToBecomeAnAcademyVisible() {
        cy.get('h1').contains('Apply to become an Academy')
    }

    static p3Visible() {
        cy.get('p').eq(3).contains('This form is for local authority maintained schools that want to become academies.')
    }

    static p4Visible() {
        cy.get('p').eq(4).contains('There is a separate process for:')
    }

    static checkSpecialSchoolsLinkVisible() {
        cy.get('li').eq(0).contains('special schools')
    }

    static checkPupilReferralUnitsLinkVisible() {
        cy.get('li').eq(1).contains('pupil referral units (PRUs)')
    }

    static p6Visible() {
        cy.get('p').eq(5).contains('Although you can invite other people to help with this application, only the chair of governors will be able to submit it.')
    }

    static h2Visible() {
        cy.get('.govuk-heading-l').contains('Before you start')
    }

    static p7Visible() {
        cy.get('p').eq(6).contains('You must:')
    }

    static consultationWithStakeholdersLinkVisible() {
        cy.get('a[target="_blank"]').eq(0).contains('carry out a consultation with your stakeholders (opens in new tab)')
    }

    static completeAnEqualityImpactAssessmentVisible() {
        cy.get('li').eq(3).contains('complete an Equality Impact Assessment (EIA)')
    }

    static provideBankDetailsLinkVisible() {
        cy.get('a[target="_blank"]').eq(1).contains('provide bank details (opens in a new tab)')
    }

    static p8Visible()
    {
        cy.get('p').eq(7).contains('You should also:')
    }

    static contactYourRegionalDirectorLinkVisible()
    {
        cy.get('a[target="_blank"]').eq(2).contains('your regional director (opens in a new tab)')
    }

    static allInformationAndEvidenceYouWillNeedLinkVisible()
    {
        cy.get('a[target="_blank"]').eq(3).contains('all of the information and evidence you will need (opens in a new tab)')
    }

    static StartNowVisible() {
        cy.get('.govuk-grid-column-two-thirds > .govuk-button').should('be.visible')
    }

    static clickStartNow() {
        cy.get('.govuk-grid-column-two-thirds > .govuk-button').click()
    }
}