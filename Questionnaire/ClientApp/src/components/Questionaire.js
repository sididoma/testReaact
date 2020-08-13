import React, { Component } from 'react';

export class Questionnaire extends Component {
    static displayName = Questionnaire.name


    constructor(props) {
        super(props);
        this.state = { questions: [], loading: true, content: "", current: -1 };
    }

    componentDidMount() {
        this.fetchQuestionsData();
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : "";
        if (!this.state.loading) {
            if (this.state.current == -1) {
                contents = <button className="button" onClick={this.updateStates(1)}>Начать заполнение анкеты</button>
            }
            if (this.state.current == 0) {
                contents = renderFirst();

            }
        }
        return (
            <div>
                {contents}
            </div>
        );
    }

     renderFirst() {
        return ([<p>{this.state.questions[0].question}</p>,
            <input type={this.state.questions[this.state.current].type}></input>])
    }

    updateStates(x) {
        let z = this.state.current;
        this.setState({
            current: this.state.current + x
        });
    }

    async fetchQuestionsData() {
        const response = await fetch('questionnaire');
        const data = await response.json();
        this.setState({ questions: data, loading: false });
    }
}