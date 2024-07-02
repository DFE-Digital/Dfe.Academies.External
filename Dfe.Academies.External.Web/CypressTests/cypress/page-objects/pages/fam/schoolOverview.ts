// TODO refactor all methods
// TODO get better selector for elements

class SchoolOverview {
  public checkAboutConversionCompleted(): this {
    cy.get('strong').eq(1).contains('Completed')

    return this
  }

  public checkFurtherInformationCompleted(): this {
    cy.get('strong').eq(2).contains('Completed')

    return this
  }

  public checkFinancesCompleted(): this {
    cy.get('strong').eq(3).contains('Completed')

    return this
  }

  public checkFuturePupilNumbersCompleted(): this {
    cy.get('strong').eq(4).contains('Completed')

    return this
  }

  public checkLandAndBuildingsCompleted(): this {
    cy.get('strong').eq(5).contains('Completed')

    return this
  }

  public checkConsultationCompleted(): this {
    cy.get('strong').eq(6).contains('Completed')

    return this
  }

  public checkPreopeningSupportGrantCompleted(): this {
    cy.get('strong').eq(7).contains('Completed')

    return this
  }

  public checkDeclarationCompleted(): this {
    cy.get('strong').eq(8).contains('Completed')

    return this
  }

  public saveAndReturn(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const schoolOverview = new SchoolOverview()

export default schoolOverview
