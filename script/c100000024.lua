--タイム・ストリーム
function c100000024.initial_effect(c)
	--Activate
	local e1=Effect.CreateEffect(c)
	e1:SetCategory(CATEGORY_SPECIAL_SUMMON)
	e1:SetProperty(EFFECT_FLAG_CARD_TARGET)
	e1:SetType(EFFECT_TYPE_ACTIVATE)
	e1:SetCode(EVENT_FREE_CHAIN)
	e1:SetCost(c100000024.cost)
	e1:SetTarget(c100000024.target)
	e1:SetOperation(c100000024.activate)
	c:RegisterEffect(e1)
end
function c100000024.cost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return true end
	local lp=Duel.GetLP(tp)
	local costlp=math.floor((lp+1)/2)
	Duel.PayLPCost(tp,costlp)
end
function c100000024.tfilter1(c,e,tp)
	return c:GetCode()==100000027 and c:IsCanBeSpecialSummoned(e,0,tp,true,false)
end
function c100000024.tfilter2(c,e,tp)
	return c:GetCode()==100000028 and c:IsCanBeSpecialSummoned(e,0,tp,true,false)
end
function c100000024.filter1(c,e,tp)
	return c:IsFaceup() and c:GetCode()==100000026
		and Duel.IsExistingMatchingCard(c100000024.tfilter1,tp,LOCATION_EXTRA,0,1,nil,e,tp)
end
function c100000024.filter2(c,e,tp)
	return c:IsFaceup() and c:GetCode()==100000027
		and Duel.IsExistingMatchingCard(c100000024.tfilter2,tp,LOCATION_EXTRA,0,1,nil,e,tp)
end

function c100000024.target(e,tp,eg,ep,ev,re,r,rp,chk,chkc)
	if chkc then return chkc:IsControler(tp) and chkc:IsLocation(LOCATION_MZONE) and c100000024.filter1(chkc,e,tp) or c100000024.filter2(chkc,e,tp)end
	if chk==0 then return Duel.GetLocationCount(tp,LOCATION_MZONE)>-1
		and Duel.IsExistingTarget(c100000024.filter1,tp,LOCATION_MZONE,0,1,nil,e,tp) 
		or Duel.IsExistingTarget(c100000024.filter2,tp,LOCATION_MZONE,0,1,nil,e,tp) end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_TODECK)
	if Duel.IsExistingTarget(c100000024.filter1,tp,LOCATION_MZONE,0,1,nil,e,tp)~=0 
	then local g=Duel.SelectTarget(tp,c100000024.filter1,tp,LOCATION_MZONE,0,1,1,nil,e,tp) end
	if Duel.IsExistingTarget(c100000024.filter2,tp,LOCATION_MZONE,0,1,nil,e,tp)~=0 
	then local g=Duel.SelectTarget(tp,c100000024.filter2,tp,LOCATION_MZONE,0,1,1,nil,e,tp) end
	Duel.SetOperationInfo(0,CATEGORY_SPECIAL_SUMMON,nil,1,tp,LOCATION_DECK)
end
function c100000024.activate(e,tp,eg,ep,ev,re,r,rp)
	local tc=Duel.GetFirstTarget()
	if not tc:IsRelateToEffect(e) then return end
	if Duel.SendtoDeck(tc,nil,1,REASON_EFFECT)==0 then return end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_SPSUMMON)
	if tc:GetCode()==100000026 then 
	local sg=Duel.SelectMatchingCard(tp,c100000024.tfilter1,tp,LOCATION_EXTRA,0,1,1,nil,e,tp) 
	Duel.SpecialSummon(sg,SUMMON_TYPE_FUSION,tp,tp,true,false,POS_FACEUP) end
	if tc:GetCode()==100000027 then 
	local sg=Duel.SelectMatchingCard(tp,c100000024.tfilter2,tp,LOCATION_EXTRA,0,1,1,nil,e,tp) 
	Duel.SpecialSummon(sg,SUMMON_TYPE_FUSION,tp,tp,true,false,POS_FACEUP) end
end
