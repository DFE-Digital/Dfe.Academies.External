// TODO refactor all methods
// TODO get better selector for elements

class SchoolOverview {
  public FAMSchoolOverviewPageNotStartedElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Form a multi-academy trust')

    cy.get('.govuk-heading-l').contains('Plymstock School')

    cy.get('.govuk-body').contains('Complete all sections.')

    cy.get('strong').eq(1).contains('Not Started')
    cy.get('strong').eq(2).contains('Not Started')
    cy.get('strong').eq(3).contains('Not Started')
    cy.get('strong').eq(4).contains('Not Started')
    cy.get('strong').eq(5).contains('Not Started')
    cy.get('strong').eq(6).contains('Not Started')
    cy.get('strong').eq(7).contains('Not Started')
    cy.get('strong').eq(8).contains('Not Started')

    cy.get('strong').eq(9).contains('The declaration must be completed by the chair of the school\'s governing body.')

    cy.get('.govuk-button').should('be.visible').contains('Save and return')

    return this
  }

  public FAMSchoolOverviewPageAboutConversionCompleteElementsVisible(): this {
    cy.get('strong').eq(1).contains('Completed')

    return this
  }

  public FAMSchoolOverviewPageFurtherInformationCompleteElementsVisible(): this {
    cy.get('strong').eq(2).contains('Completed')

    return this
  }

  public FAMSchoolOverviewPageFinancesCompleteElementsVisible(): this {
    cy.get('strong').eq(3).contains('Completed')

    return this
  }

  public FAMSchoolOverviewPageFuturePupilNumbersCompleteElementsVisible(): this {
    cy.get('strong').eq(4).contains('Completed')

    return this
  }

  public FAMSchoolOverviewPageLandAndBuildingsCompleteElementsVisible(): this {
    cy.get('strong').eq(5).contains('Completed')

    return this
  }

  public FAMSchoolOverviewPageConsultationCompleteElementsVisible(): this {
    cy.get('strong').eq(6).contains('Completed')

    return this
  }

  public FAMSchoolOverviewPagePreopeningSupportGrantCompleteElementsVisible(): this {
    cy.get('strong').eq(7).contains('Completed')

    return this
  }

  public FAMSchoolOverviewPageDeclarationCompleteElementsVisible(): this {
    cy.get('strong').eq(8).contains('Completed')

    return this
  }

  public selectSaveAndReturn(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const schoolOverview = new SchoolOverview()

export default schoolOverview
