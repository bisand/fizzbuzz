import React, { Component } from 'react';

export class CustomRules extends Component {
  static displayName = CustomRules.name;

  constructor(props) {
    super(props);
    this.state = { numbers: [], loading: true, ruleSet: [{ rules: [{ number: undefined, word: undefined }], from: undefined, to: undefined }] };
  }

  componentDidMount() {
    // this.populateWeatherData();
  }

  static renderNumbersTable(numbers) {
    let key = 1
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Number</th>
            <th>Processed Numbers</th>
          </tr>
        </thead>
        <tbody>
          {numbers.map(num =>
            <tr key={key++}>
              <td>{num.number}</td>
              <td>{num.word}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  addRuleSet(e) {
    e.preventDefault();
    this.state.ruleSet.push({ rules: [{ number: undefined, word: undefined }], from: undefined, to: undefined });
    this.forceUpdate();
  }

  addRule(e, ruleSet) {
    e.preventDefault();
    ruleSet.rules.push({ number: undefined, word: undefined });
    this.forceUpdate();
  }

  clearRuleSet(e) {
    e.preventDefault();
    this.setState({ numbers: [], loading: true, ruleSet: [{ rules: [{ number: undefined, word: undefined }], from: undefined, to: undefined }] });
    this.forceUpdate();
  }

  handleRuleInputChange(ruleSetIdx, ruleIdx, event) {
    const values = this.state.ruleSet;
    const updatedValue = event.target.name;
    values[ruleSetIdx].rules[ruleIdx][updatedValue] = event.target.value;

    this.forceUpdate();
  };

  handleRuleSetInputChange(ruleSetIdx, event) {
    const values = this.state.ruleSet;
    const updatedValue = event.target.name;
    values[ruleSetIdx][updatedValue] = event.target.value;

    this.forceUpdate();
  };

  render() {
    let contents = this.state.loading
      ? <p><em>No data</em></p>
      : CustomRules.renderNumbersTable(this.state.numbers);

    let ruleSetForm = (
      <div className="App">
        <form>
          <div className='container'>
            <div className='row'>
              <div className='col'>
                {this.state.ruleSet.map((ruleSet, ruleSetIdx) => {
                  return <div key={ruleSetIdx} className='card bg-light mb-3'>
                    <div className='card-header'>Ruleset {ruleSetIdx + 1}</div>
                    <div className='card-body'>
                      {ruleSet.rules.map((rule, ruleIdx) => {
                        return <div key={ruleIdx} className='card bg-light mb-3'>
                          <div className='card-header'>Rule {ruleIdx + 1}</div>
                          <div className='card-body'>
                            <div className='row'>
                              <div className='col'>
                                <input name='number' placeholder='Number' pattern='[0-9]*' defaultValue={rule.number} onChange={(e) => this.handleRuleInputChange(ruleSetIdx, ruleIdx, e)} className='form-control' />
                              </div>
                              <div className='col'>
                                <input name='word' placeholder='Word' defaultValue={rule.word} onChange={(e) => this.handleRuleInputChange(ruleSetIdx, ruleIdx, e)} className='form-control' />
                              </div>
                            </div>
                          </div>
                        </div>
                      })}
                      <button onClick={(e) => this.addRule(e, ruleSet)} className='btn btn-primary'>Add Rule</button>
                      <div className='row mt-3'>
                        <div className='col'>
                          <input name='from' placeholder='From' defaultValue={ruleSet.from} onChange={(e) => this.handleRuleSetInputChange(ruleSetIdx, e)} className='form-control' />
                        </div>
                        <div className='col'>
                          <input name='to' placeholder='To' defaultValue={ruleSet.to} onChange={(e) => this.handleRuleSetInputChange(ruleSetIdx, e)} className='form-control' />
                        </div>
                      </div>
                    </div>
                  </div>
                })
                }
                <button onClick={(e) => this.addRuleSet(e)} className='btn btn-primary'>Add Ruleset</button>
              </div>
              <div className='col'>
                <div className='card bg-light mb-3'>
                  <div className='card-header'>Rules</div>
                  <div className='card-body'>{JSON.stringify(this.state.ruleSet)}</div>
                </div>
                <button onClick={(e) => this.clearRuleSet(e)} className='btn btn-danger'>Clear Ruleset</button>
              </div>
            </div>
          </div>
        </form >
      </div>
    );

    return (
      <div>
        <h1 id="tabelLabel" >Numbers</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {ruleSetForm}
        <div className='container'>
          <div className='row'>
            <div className='col'>
              <hr></hr>
              <button onClick={(e) => this.populateCustomRulesData(e)} className='btn btn-success'>Execute Ruleset</button>
              <div className='card bg-light mb-3 mt-3'>
                <div className='card-header'>Result {this.state.rulesName}</div>
                <div className='card-body'>
                  {contents}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }

  async fetchData(ruleSet) {
    const result = await Promise.all(ruleSet.map(rs => {
      const url = `numbers/custom-full?from=${rs.from}&to=${rs.to}`;
      return fetch(url, {
        method: 'POST',
        body: JSON.stringify(rs.rules.map((rule, ruleIdx) => {
          rule.number = Number(rule.number);
          return rule;
        })),
        headers: { 'Content-Type': 'application/json' },
      })
    }))
      .then(async (res) => {
        return Promise.all(
          res.map(async (data) => {
            const arr = await data.json();
            return arr;
          })
        )
      }).catch(error => {
        console.log("Error", error);
      });
    console.log(result)
    return result.flatMap(data => data);
  }

  async populateCustomRulesData(e) {
    e.preventDefault();
    this.setState({ numbers: [], loading: true });

    const data = await this.fetchData(this.state.ruleSet)
    this.setState({ numbers: data, loading: false });
  }
}
